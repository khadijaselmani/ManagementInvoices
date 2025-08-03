import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import ProductList from './components/products/productList';
import ProductForm from './components/products/ProductForm';
// Placeholder for invoices page
const Invoices = () => <div className="container"><h2>Invoices Page (Coming Soon)</h2></div>;

const App = () => {
  return (
    <Router>
      <Navbar />
      <Routes>
        <Route path="/products" element={<ProductList />} />
        <Route path="/products/create" element={<ProductForm />} />
        <Route path="/products/edit/:id" element={<ProductForm />} />
        <Route path="/invoices" element={<Invoices />} />
        <Route path="*" element={<ProductList />} />
      </Routes>
    </Router>
  );
};

export default App;
