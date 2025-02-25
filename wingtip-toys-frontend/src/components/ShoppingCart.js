import React, { useState, useEffect } from 'react'; 
import axios from 'axios'; 
const ShoppingCart = () => { 
  const [cartItems, setCartItems] = useState([]); 
  const [total, setTotal] = useState(0); 
  useEffect(() => { 
    axios.get('/api/cart') 
      .then(response => { 
        console.log('Fetched cart items:', response.data); 
        setCartItems(response.data.items); 
        setTotal(response.data.total); 
      }) 
      .catch(error => { 
        console.error('Error fetching cart items:', error); 
      }); 
  }, []); 
  const updateCart = () => { 
    axios.post('/api/cart', { items: cartItems }) 
      .then(response => { 
        console.log('Cart updated:', response.data); 
        setCartItems(response.data.items); 
        setTotal(response.data.total); 
      }) 
      .catch(error => { 
        console.error('Error updating cart:', error); 
      }); 
  }; 
  const handleQuantityChange = (productId, quantity) => { 
    const updatedItems = cartItems.map(item => 
      item.productID === productId ? { ...item, quantity: parseInt(quantity, 10) } : item 
    ); 
    setCartItems(updatedItems); 
  }; 
  const handleRemoveItem = (productId) => { 
    const updatedItems = cartItems.filter(item => item.productID !== productId); 
    setCartItems(updatedItems); 
  }; 
  return ( 
    <div> 
      <h1>Shopping Cart</h1> 
      {cartItems.length === 0 ? ( 
        <div>Your cart is empty</div> 
      ) : ( 
        <div> 
          <table className="table"> 
            <thead> 
              <tr> 
                <th>ID</th> 
                <th>Name</th> 
                <th>Price (each)</th> 
                <th>Quantity</th> 
                <th>Item Total</th> 
                <th>Remove</th> 
              </tr> 
            </thead> 
            <tbody> 
              {cartItems.map(item => ( 
                <tr key={item.productID}> 
                  <td>{item.productID}</td> 
                  <td>{item.product.productName}</td> 
                  <td>${item.product.unitPrice}</td> 
                  <td> 
                    <input 
                      type="number" 
                      value={item.quantity} 
                      onChange={e => handleQuantityChange(item.productID, e.target.value)} 
                    /> 
                  </td> 
                  <td>${(item.quantity * item.product.unitPrice).toFixed(2)}</td> 
                  <td> 
                    <input 
                      type="checkbox" 
                      onChange={() => handleRemoveItem(item.productID)} 
                    /> 
                  </td> 
                </tr> 
              ))} 
            </tbody> 
          </table> 
          <div> 
            <strong>Order Total: ${total.toFixed(2)}</strong> 
          </div> 
          <button onClick={updateCart}>Update Cart</button> 
        </div> 
      )} 
    </div> 
  ); 
}; 
export default ShoppingCart; 