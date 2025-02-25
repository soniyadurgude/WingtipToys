import React from 'react'; 
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'; 
import ProductList from './components/ProductList'; 
import ProductDetails from './components/ProductDetails'; 
import ShoppingCart from './components/ShoppingCart'; 
function App() { 
  return ( 
    <Router> 
      <div className="App"> 
        <Switch> 
          <Route path="/" exact component={ProductList} /> 
          <Route path="/product-details/:productId" component={ProductDetails} /> 
          <Route path="/shopping-cart" component={ShoppingCart} /> 
          {/* Add more routes as needed */} 
        </Switch> 
      </div> 
    </Router> 
  ); 
} 
export default App; 