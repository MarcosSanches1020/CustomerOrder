import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import api from '../services/api'
import type { Customer, CustomerCreate } from '../types'

export default function CustomerEditPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [form, setForm] = useState<CustomerCreate>({ name: '', cpf: '' })
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const load = async () => {
      try {
        const { data } = await api.get<Customer>(`/Customer/${id}`)
        setForm({ name: data.name, cpf: data.cpf })
      } catch (err: any) {
        setError(err?.message ?? 'Erro ao carregar cliente')
      } finally {
        setLoading(false)
      }
    }
    load()
  }, [id])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setSaving(true)
    setError(null)
    try {
      await api.put(`/Customer/update/${id}`, form)
      navigate('/customers')
    } catch (err: any) {
      setError(err?.response?.data ?? err?.message ?? 'Erro ao salvar cliente')
    } finally {
      setSaving(false)
    }
  }

  if (loading) return <p>Carregando...</p>

  return (
    <div>
      <h2>Editar Cliente</h2>
      {error && <p style={{ color: 'red' }}>{String(error)}</p>}
      <form onSubmit={handleSubmit} style={{ display: 'grid', gap: 12, maxWidth: 480 }}>
        <label>
          Nome
          <input name="name" value={form.name} onChange={handleChange} required />
        </label>
        <label>
          CPF
          <input name="cpf" value={form.cpf} onChange={handleChange} required maxLength={11} />
        </label>
        <div style={{ display: 'flex', gap: 8 }}>
          <button type="submit" disabled={saving}>{saving ? 'Salvando...' : 'Salvar'}</button>
          <button type="button" onClick={() => navigate('/customers')}>Cancelar</button>
        </div>
      </form>
    </div>
  )
}


