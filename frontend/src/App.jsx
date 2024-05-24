import { useState } from "react";
import { useLocation } from "react-router-dom";
import { Space } from "antd";
import "./App.css";
import Header from "./Components/Header";
import SideMenu from "./Components/SideMenu";
import PageContent from "./Components/PageContent";
import Footer from "./Components/Footer";
import Login from "./Pages/Login"
import Register from "./Pages/Register"

function App() {
  // State to track if user is logged in
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const location = useLocation();

  if(isLoggedIn) {
    return (
      <div className="app">
      <Header />
        <Space className="main-container">
          <SideMenu />
          <PageContent />
        </Space>
        <Footer />
      </div>
      );
  }
  else {
    if (location.pathname === "/") {
      return (
        <div style={{height: "100dvh", width: "100dvw" , display: "flex", justifyContent: "center", alignItems: "center"}}>
          <Login />
        </div>
      );
    }
    else {
      return (
        <div style={{height: "100dvh", width: "100dvw" , display: "flex", justifyContent: "center", alignItems: "center"}}>
          <Register />
        </div>
      );
    }
    
  }
    
    
      

}

export default App;
