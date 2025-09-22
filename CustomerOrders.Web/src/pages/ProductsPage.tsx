import { useEffect, useState } from 'react'
import api from '../services/api'
import type { Product } from '../types'

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const { data } = await api.get<Product[]>('/Product')
        setProducts(data)
      } catch (err: any) {
        setError(err?.message ?? 'Erro ao carregar produtos')
      } finally {
        setLoading(false)
      }
    }
    fetchProducts()
  }, [])

  if (loading) return <p>Carregando...</p>
  if (error) return <p style={{ color: 'red' }}>{error}</p>

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2>Products</h2>
        <a href="/products/new">+ Novo</a>
      </div>
      {products.length === 0 ? (
        <p>Nenhum produto encontrado.</p>
      ) : (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>ID</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Nome</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Preço</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Tipo</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Descrição</th>
            </tr>
          </thead>
          <tbody>
            {products.map((p) => (
              <tr key={p.id}>
                <td style={{ padding: 8 }}>{p.id}</td>
                <td style={{ padding: 8 }}>{p.nameProduct}</td>
                <td style={{ padding: 8 }}>{p.priceProduct.toLocaleString(undefined, { style: 'currency', currency: 'BRL' })}</td>
                <td style={{ padding: 8 }}>{p.typeProduct}</td>
                <td style={{ padding: 8 }}>{p.description}</td>
                <td style={{ padding: 8 }}>
                  <a href={`/products/edit/${p.id}`} style={{ marginRight: 8 }}>Editar</a>
                  <button
                    onClick={async () => {
                      if (!confirm('Deseja realmente excluir?')) return
                      await api.delete(`/Product/delete/${p.id}`)
                      setProducts((prev) => prev.filter((x) => x.id !== p.id))
                    }}
                  >
                    Excluir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  )
}


