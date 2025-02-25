import React, { useEffect } from 'react'; 
import { useHistory, useParams } from 'react-router-dom'; 
import axios from 'axios'; 
const AddToCart = () => { 
  const { productId } = useParams(); 
  const history = useHistory(); 
  useEffect(() => { 
    const addToCart = async () => { 
      try { 
        if (productId) { 
          console.log(`Adding product with ID ${productId} to cart.`); 
          await axios.post(`/api/cart/add`, { productId }); 
          console.log(`Product with ID ${productId} added to cart successfully.`); 
          history.push('/shopping-cart'); 
        } else { 
          console.error('ERROR: Product ID is required to add to cart.'); 
          throw new Error('Product ID is required to add to cart.'); 
        } 
      } catch (error) { 
        console.error('Error adding product to cart:', error.message, error.stack); 
      } 
    }; 
    addToCart(); 
  }, [productId, history]); 
  return null; 
}; 
export default AddToCart; 