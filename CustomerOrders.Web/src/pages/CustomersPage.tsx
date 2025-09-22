import { useEffect, useState } from 'react'
import api from '../services/api'
import type { Customer } from '../types'

export default function CustomersPage() {
  const [customers, setCustomers] = useState<Customer[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const { data } = await api.get<Customer[]>('/Customer')
        setCustomers(data)
      } catch (err: any) {
        setError(err?.message ?? 'Erro ao carregar clientes')
      } finally {
        setLoading(false)
      }
    }
    fetchCustomers()
  }, [])

  if (loading) return <p>Carregando...</p>
  if (error) return <p style={{ color: 'red' }}>{error}</p>

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2>Customers</h2>
        <a href="/customers/new">+ Novo</a>
      </div>
      {customers.length === 0 ? (
        <p>Nenhum cliente encontrado.</p>
      ) : (
        <table style={{ width: '100%', borderCollapse: 'collapse' }}>
          <thead>
            <tr>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>ID</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Nome</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>CPF</th>
              <th style={{ textAlign: 'left', borderBottom: '1px solid #ddd' }}>Cadastro</th>
            </tr>
          </thead>
          <tbody>
            {customers.map((c) => (
              <tr key={c.id}>
                <td style={{ padding: 8 }}>{c.id}</td>
                <td style={{ padding: 8 }}>{c.name}</td>
                <td style={{ padding: 8 }}>{c.cpf}</td>
                <td style={{ padding: 8 }}>{new Date(c.dataRegister).toLocaleString()}</td>
                <td style={{ padding: 8 }}>
                  <a href={`/customers/edit/${c.id}`} style={{ marginRight: 8 }}>Editar</a>
                  <button
                    onClick={async () => {
                      if (!confirm('Deseja realmente excluir?')) return
                      await api.delete(`/Customer/delete/${c.id}`)
                      setCustomers((prev) => prev.filter((x) => x.id !== c.id))
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


