<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSwitcher.ascx.cs" Inherits="WingtipToys.ViewSwitcher" %> 
<div id="viewSwitcher"> 
    <%: CurrentView %> view | <a href="<%: SwitchUrl %>" data-ajax="false">Switch to <%: AlternateView %></a> 
</div> 
<div id="reactAppContainer"> 
    <!-- React app will be injected here --> 
</div> 
<script src="/path-to-react-app/build/static/js/main.js"></script>