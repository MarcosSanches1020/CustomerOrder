import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import api from '../services/api'
import type { CustomerCreate } from '../types'

export default function CustomerFormPage() {
  const navigate = useNavigate()
  const [form, setForm] = useState<CustomerCreate>({ name: '', cpf: '' })
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setSaving(true)
    setError(null)
    try {
      await api.post('/Customer', form)
      navigate('/customers')
    } catch (err: any) {
      setError(err?.response?.data ?? err?.message ?? 'Erro ao salvar cliente')
    } finally {
      setSaving(false)
    }
  }

  return (
    <div>
      <h2>Novo Cliente</h2>
      {error && <p style={{ color: 'red' }}>{String(error)}</p>}
      <form onSubmit={handleSubmit} style={{ display: 'grid', gap: 12, maxWidth: 480 }}>
        <label>
          Nome
          <input name="name" value={form.name} onChange={handleChange} placeholder="Nome" required />
        </label>
        <label>
          CPF
          <input name="cpf" value={form.cpf} onChange={handleChange} placeholder="Somente números" required maxLength={11} />
        </label>
        <div style={{ display: 'flex', gap: 8 }}>
          <button type="submit" disabled={saving}>{saving ? 'Salvando...' : 'Salvar'}</button>
          <button type="button" onClick={() => navigate('/customers')}>Cancelar</button>
        </div>
      </form>
    </div>
  )
}


