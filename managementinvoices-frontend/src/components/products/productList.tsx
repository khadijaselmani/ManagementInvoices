import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getProducts, deleteProduct } from '../../services/productService';

type Product = {
  id: string;
  name: string;
  price: number;
};

const ProductList = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);

  const fetchProducts = async () => {
    try {
      const data = await getProducts();
      setProducts(data);
    } catch (error) {
      alert('Failed to fetch products');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (!window.confirm('Are you sure you want to delete this product?')) return;
    try {
      await deleteProduct(id);
      fetchProducts();
    } catch (error) {
      alert('Failed to delete product');
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  if (loading) return <div className="container">Loading products...</div>;

  return (
    <div className="container">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Products</h2>
        <Link to="/products/create" className="btn btn-primary">Create Product</Link>
      </div>

      <table className="table table-striped table-bordered">
        <thead className="table-dark">
          <tr>
            <th>Name</th>
            <th>Price ($)</th>
            <th style={{width: "150px"}}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.length === 0 ? (
            <tr><td colSpan={3} className="text-center">No products found.</td></tr>
          ) : (
            products.map(p => (
              <tr key={p.id}>
                <td>{p.name}</td>
                <td>{p.price.toFixed(2)}</td>
                <td>
                  <Link to={`/products/edit/${p.id}`} className="btn btn-sm btn-warning me-2">Edit</Link>
                  <button className="btn btn-sm btn-danger" onClick={() => handleDelete(p.id)}>Delete</button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ProductList;
