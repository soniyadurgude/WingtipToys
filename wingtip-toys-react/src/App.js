import React from 'react'; 
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'; 
import ProductList from './components/ProductList'; 
const App = () => { 
  return ( 
    <Router> 
      <div> 
        <Switch> 
          <Route path="/" exact component={ProductList} /> 
          {/* Add more routes as needed for other components */} 
        </Switch> 
      </div> 
    </Router> 
  ); 
}; 
export default App; 