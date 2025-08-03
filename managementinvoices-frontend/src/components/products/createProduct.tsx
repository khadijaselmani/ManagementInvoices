import React, { useState } from 'react';
import { createProduct } from '../../services/productService';
import { useNavigate } from 'react-router-dom';

const CreateProduct: React.FC = () => {
  const [name, setName] = useState('');
  const [price, setPrice] = useState<number | ''>('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim() || price === '') {
      alert('Please enter name and price');
      return;
    }
    try {
      await createProduct({ name, price: Number(price) });
      alert('Product created successfully');
      navigate('/products'); // Redirect back to product list
    } catch (error) {
      console.error('Error creating product:', error);
      alert('Failed to create product');
    }
  };

  return (
    <div>
      <h2>Create Product</h2>
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
        <button type="submit">Create</button>
      </form>
    </div>
  );
};

export default CreateProduct;
