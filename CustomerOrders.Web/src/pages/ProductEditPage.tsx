import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import api from '../services/api'
import type { Product, ProductCreate } from '../types'

export default function ProductEditPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [form, setForm] = useState<ProductCreate>({
    nameProduct: '',
    priceProduct: 0,
    typeProduct: '',
    description: '',
  })
  const [loading, setLoading] = useState(true)
  const [saving, setSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const load = async () => {
      try {
        const { data } = await api.get<Product>(`/Product/${id}`)
        setForm({
          nameProduct: data.nameProduct,
          priceProduct: data.priceProduct,
          typeProduct: data.typeProduct,
          description: data.description,
        })
      } catch (err: any) {
        setError(err?.message ?? 'Erro ao carregar produto')
      } finally {
        setLoading(false)
      }
    }
    load()
  }, [id])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target
    setForm((prev) => ({
      ...prev,
      [name]: name === 'priceProduct' ? Number(value) : value,
    }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setSaving(true)
    setError(null)
    try {
      await api.put(`/Product/update/${id}`, form)
      navigate('/products')
    } catch (err: any) {
      setError(err?.response?.data ?? err?.message ?? 'Erro ao salvar produto')
    } finally {
      setSaving(false)
    }
  }

  if (loading) return <p>Carregando...</p>

  return (
    <div>
      <h2>Editar Produto</h2>
      {error && <p style={{ color: 'red' }}>{String(error)}</p>}
      <form onSubmit={handleSubmit} style={{ display: 'grid', gap: 12, maxWidth: 520 }}>
        <label>
          Nome
          <input name="nameProduct" value={form.nameProduct} onChange={handleChange} required />
        </label>
        <label>
          Preço
          <input name="priceProduct" type="number" step="0.01" value={form.priceProduct} onChange={handleChange} required />
        </label>
        <label>
          Tipo
          <input name="typeProduct" value={form.typeProduct} onChange={handleChange} required />
        </label>
        <label>
          Descrição
          <textarea name="description" value={form.description} onChange={handleChange} rows={3} />
        </label>
        <div style={{ display: 'flex', gap: 8 }}>
          <button type="submit" disabled={saving}>{saving ? 'Salvando...' : 'Salvar'}</button>
          <button type="button" onClick={() => navigate('/products')}>Cancelar</button>
        </div>
      </form>
    </div>
  )
}


