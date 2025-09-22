export type Customer = {
  id: number
  name: string
  cpf: string
  dataRegister: string
  dataUpdate?: string | null
}

export type CustomerCreate = {
  name: string
  cpf: string
}

export type Product = {
  id: number
  nameProduct: string
  priceProduct: number
  typeProduct: string
  description: string
  dataRegister: string
  dataUpdate?: string | null
}

export type ProductCreate = {
  nameProduct: string
  priceProduct: number
  typeProduct: string
  description: string
}


