import React, { useState, useEffect } from 'react'; 
import axios from 'axios'; 
const ProductList = () => { 
  const [products, setProducts] = useState([]); 
  useEffect(() => { 
    axios.get('/api/products') // Assuming the API endpoint for products 
      .then(response => { 
        console.log('Products fetched successfully:', response.data); 
        setProducts(response.data); 
      }) 
      .catch(error => { 
        console.error('Error fetching products:', error); 
      }); 
  }, []); 
  return ( 
    <div> 
      <h2>Products</h2> 
      <div className="product-list"> 
        {products.map(product => ( 
          <div key={product.productID} className="product-item"> 
            <a href={`/product-details/${product.productID}`}> 
              <img src={`/catalog/images/thumbs/${product.imagePath}`} alt={product.productName} width="100" height="75" /> 
            </a> 
            <div> 
              <a href={`/product-details/${product.productID}`}> 
                <span>{product.productName}</span> 
              </a> 
              <br /> 
              <span><b>Price: </b>${product.unitPrice}</span> 
              <br /> 
              <a href={`/add-to-cart/${product.productID}`}> 
                <span className="product-list-item"> 
                  <b>Add to Cart</b> 
                </span> 
              </a> 
            </div> 
          </div> 
        ))} 
      </div> 
    </div> 
  ); 
}; 
export default ProductList; 