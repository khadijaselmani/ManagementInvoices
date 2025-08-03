import React from 'react';
import { deleteProduct } from '../../services/productService';
import { useParams, useNavigate } from 'react-router-dom';

const DeleteProduct: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const handleDelete = async () => {
    if (id && window.confirm('Are you sure you want to delete this product?')) {
      try {
        await deleteProduct(id);
        alert('Product deleted successfully');
        navigate('/products');
      } catch (error) {
        console.error('Failed to delete product:', error);
        alert('Failed to delete product');
      }
    } else {
      navigate('/products');
    }
  };

  React.useEffect(() => {
    handleDelete();
  }, []); // Run once on mount

  return <div>Deleting product...</div>;
};

export default DeleteProduct;
