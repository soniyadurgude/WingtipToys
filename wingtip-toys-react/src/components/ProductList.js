import React, { useEffect, useState } from 'react'; 
import { getProducts } from '../services/apiService'; 
const ProductList = () => { 
  const [products, setProducts] = useState([]); 
  useEffect(() => { 
    const fetchProducts = async () => { 
      try { 
        const data = await getProducts(); 
        setProducts(data); 
      } catch (error) { 
        console.error('Error fetching products:', error); 
      } 
    }; 
    fetchProducts(); 
  }, []); 
  return ( 
    <div> 
      <h2>Products</h2> 
      <ul> 
        {products.map(product => ( 
          <li key={product.productID}> 
            <h3>{product.productName}</h3> 
            <p>{product.description}</p> 
            <p>Price: ${product.unitPrice}</p> 
          </li> 
        ))} 
      </ul> 
    </div> 
  ); 
}; 
export default ProductList; 