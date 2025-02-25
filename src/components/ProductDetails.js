// src/components/ProductDetails.js 
import React, { useState, useEffect } from 'react'; 
import axios from 'axios'; 
import { useParams } from 'react-router-dom'; 
const ProductDetails = () => { 
  const { productId } = useParams(); 
  const [product, setProduct] = useState(null); 
  useEffect(() => { 
    axios.get(`/api/products/${productId}`) 
      .then(response => setProduct(response.data)) 
      .catch(error => console.error('Error fetching product details:', error)); 
  }, [productId]); 
  if (!product) return <div>Loading...</div>; 
  return ( 
    <div> 
      <h1>{product.productName}</h1> 
      <img src={`/catalog/images/${product.imagePath}`} alt={product.productName} style={{ border: 'solid', height: '300px' }} /> 
      <div> 
        <b>Description:</b><br />{product.description} 
        <br /> 
        <span><b>Price:</b> ${product.unitPrice}</span> 
        <br /> 
        <span><b>Product Number:</b> {product.productID}</span> 
      </div> 
    </div> 
  ); 
}; 
export default ProductDetails; 