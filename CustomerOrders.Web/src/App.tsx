import { BrowserRouter, Routes, Route, NavLink } from 'react-router-dom'
import CustomersPage from './pages/CustomersPage'
import ProductsPage from './pages/ProductsPage.tsx'
import CustomerFormPage from './pages/CustomerFormPage'
import ProductFormPage from './pages/ProductFormPage'
import CustomerEditPage from './pages/CustomerEditPage'
import ProductEditPage from './pages/ProductEditPage'

function App() {
  return (
    <BrowserRouter>
      <div style={{ maxWidth: 960, margin: '0 auto', padding: 24 }}>
        <header style={{ display: 'flex', gap: 16, marginBottom: 24 }}>
          <NavLink to="/" end>
            Home
          </NavLink>
          <NavLink to="/customers">Customers</NavLink>
          <NavLink to="/products">Products</NavLink>
        </header>
        <Routes>
          <Route
            path="/"
            element={
              <div>
                <h1>Customer Orders</h1>
                <p>Bem-vindo! Use o menu para navegar.</p>
              </div>
            }
          />
          <Route path="/customers" element={<CustomersPage />} />
          <Route path="/customers/new" element={<CustomerFormPage />} />
          <Route path="/customers/edit/:id" element={<CustomerEditPage />} />
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/products/new" element={<ProductFormPage />} />
          <Route path="/products/edit/:id" element={<ProductEditPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  )
}

export default App
