import React, { useState, useEffect } from 'react';
import { getProductById, updateProduct } from '../../services/productService';
import { useParams, useNavigate } from 'react-router-dom';

const EditProduct: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [name, setName] = useState('');
  const [price, setPrice] = useState<number | ''>('');
  const navigate = useNavigate();

  useEffect(() => {
    if (id) {
      getProductById(id)
        .then(product => {
          setName(product.name);
          setPrice(product.price);
        })
        .catch(err => {
          console.error('Failed to load product', err);
          alert('Product not found');
          navigate('/products');
        });
    }
  }, [id, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim() || price === '') {
      alert('Please enter name and price');
      return;
    }
    try {
      await updateProduct({
      id:id!,
      name:name,
      price: Number(price),
    });
      alert('Product updated successfully');
      navigate('/products');
    } catch (error) {
      console.error('Error updating product:', error);
      alert('Failed to update product');
    }
  };

  return (
    <div>
      <h2>Edit Product</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Name: </label>
          <input 
            type="text" 
            value={name} 
            onChange={e => setName(e.target.value)} 
            required 
          />
        </div>
        <div>
          <label>Price: </label>
          <input 
            type="number" 
            value={price} 
            onChange={e => setPrice(e.target.value === '' ? '' : Number(e.target.value))} 
            required 
            min="0"
            step="0.01"
          />
        </div>
        <button type="submit">Update</button>
      </form>
    </div>
  );
};

export default EditProduct;
