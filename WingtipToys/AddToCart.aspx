<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToCart.aspx.cs" Inherits="WingtipToys.AddToCart" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml"> 
<head runat="server"> 
    <title>Add to Cart</title> 
    <script src="/path-to-react-app/build/static/js/main.js"></script> 
</head> 
<body> 
    <form id="form1" runat="server"> 
        <div id="reactAppContainer"> 
            <!-- React app will be injected here --> 
        </div> 
    </form> 
</body> 
</html>