import axios from 'axios'; 
const API_BASE_URL = 'http://localhost:PORT/api'; // INPUT_REQUIRED {Replace PORT with your backend port number} 
export const getProducts = async () => { 
  try { 
    console.log('Fetching products from API...'); 
    const response = await axios.get(`${API_BASE_URL}/products`); 
    console.log('Products fetched successfully:', response.data); 
    return response.data; 
  } catch (error) { 
    console.error('Error fetching products:', error.message, error.stack); 
    throw error; 
  } 
}; 
export const getProductDetails = async (productId) => { 
  try { 
    console.log(`Fetching details for product ID: ${productId}`); 
    const response = await axios.get(`${API_BASE_URL}/products/${productId}`); 
    console.log('Product details fetched successfully:', response.data); 
    return response.data; 
  } catch (error) { 
    console.error('Error fetching product details:', error.message, error.stack); 
    throw error; 
  } 
}; 
export const addToCart = async (productId) => { 
  try { 
    console.log(`Adding product ID: ${productId} to cart`); 
    const response = await axios.post(`${API_BASE_URL}/cart`, { productId }); 
    console.log('Product added to cart successfully:', response.data); 
    return response.data; 
  } catch (error) { 
    console.error('Error adding to cart:', error.message, error.stack); 
    throw error; 
  } 
}; 
export const getCartItems = async () => { 
  try { 
    console.log('Fetching cart items from API...'); 
    const response = await axios.get(`${API_BASE_URL}/cart`); 
    console.log('Cart items fetched successfully:', response.data); 
    return response.data; 
  } catch (error) { 
    console.error('Error fetching cart items:', error.message, error.stack); 
    throw error; 
  } 
}; 