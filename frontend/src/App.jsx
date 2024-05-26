import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { Space } from "antd";
import "./App.css";
import Header from "./Components/Header";
import SideMenu from "./Components/SideMenu";
import PageContent from "./Components/PageContent";
// import Footer from "./Components/Footer";
import Login from "./Pages/Login"
import Register from "./Pages/Register"
import { useNavigate } from "react-router-dom";

function App() {
  // State to track if user is logged in
  const [isLoggedIn] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();
  useEffect(() => {
    if (localStorage.getItem("token") === null && sessionStorage.getItem("token") === null && location.pathname !== "/Register") {
      navigate("/");
    }
  }, [navigate, location.pathname]);


  if(isLoggedIn || localStorage.getItem("token") !== null || sessionStorage.getItem("token") !== null) {
    return (
      <div className="app">
      <Header />
        <Space className="main-container">
          <SideMenu />
          <PageContent />
        </Space>
        {/* <Footer /> */}
      </div>
      );
  }
  else {
    if (location.pathname === "/Register") {
      return (
        <div style={{height: "100dvh", width: "100dvw" , display: "flex", justifyContent: "center", alignItems: "center"}}>
          <Register />
        </div>
      );
    }
    else {
      return (
        <div style={{height: "100dvh", width: "100dvw" , display: "flex", justifyContent: "center", alignItems: "center"}}>
          <Login />
        </div>
      );
      
    }
    
  }
    
    
      

}

export default App;
