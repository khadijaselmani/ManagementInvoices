import axios from 'axios';

const API_URL = 'https://localhost:7267/api/products';

export const getProducts = async () => {
  const response = await axios.get(API_URL);
  return response.data;
};

export const getProductById = async (id: string) => {
  const response = await axios.get(`${API_URL}/${id}`);
  return response.data;
};

export const createProduct = async (product: { name: string; price: number }) => {
  const response = await axios.post(API_URL, product);
  return response.data;
};
export async function updateProduct(product: { id: string; name: string; price: number }) {
  const response = await axios.put(API_URL, product);
  return response.data;
}

export const deleteProduct = async (id: string) => {
  await axios.delete(`${API_URL}/${id}`);
};
