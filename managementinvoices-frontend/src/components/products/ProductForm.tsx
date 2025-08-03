import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { createProduct, getProductById, updateProduct } from '../../services/productService';

type Product = {
  id?: string;
  name: string;
  price: number;
};

const ProductForm = () => {
  const { id } = useParams<{ id: string }>();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [product, setProduct] = useState<Product>({ name: '', price: 0 });
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isEdit) {
      setLoading(true);
      getProductById(id!)
        .then(data => setProduct(data))
        .catch(() => alert('Failed to fetch product'))
        .finally(() => setLoading(false));
    }
  }, [id, isEdit]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setProduct({ ...product, [e.target.name]: e.target.name === 'price' ? +e.target.value : e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!product.name || product.price <= 0) {
      alert('Please enter valid product name and price.');
      return;
    }

    try {
      if (isEdit) {
        await updateProduct({
        id: product.id!,
        name: product.name,
        price: Number(product.price),
        });
        alert('Product updated!');
      } else {
        await createProduct(product);
        alert('Product created!');
      }
      navigate('/products');
    } catch {
      alert('Operation failed');
    }
  };

  if (loading) return <div className="container">Loading...</div>;

  return (
    <div className="container" style={{ maxWidth: '600px' }}>
      <h2>{isEdit ? 'Edit Product' : 'Create Product'}</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="name" className="form-label">Name</label>
          <input
            id="name"
            name="name"
            className="form-control"
            value={product.name}
            onChange={handleChange}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="price" className="form-label">Price ($)</label>
          <input
            id="price"
            name="price"
            type="number"
            className="form-control"
            value={product.price}
            onChange={handleChange}
            required
            min={0.01}
            step={0.01}
          />
        </div>

        <button type="submit" className="btn btn-success">{isEdit ? 'Update' : 'Create'}</button>
        <button type="button" className="btn btn-secondary ms-2" onClick={() => navigate('/products')}>
          Cancel
        </button>
      </form>
    </div>
  );
};

export default ProductForm;
