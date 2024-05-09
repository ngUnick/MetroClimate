import { Space } from "antd";
import "./App.css";
import Header from "./Components/Header";
import SideMenu from "./Components/SideMenu";
import PageContent from "./Components/PageContent";
import Footer from "./Components/Footer";

function App() {
  return (
    <div className="app">
      <Header />
      <Space className="main-container">
        <SideMenu></SideMenu>
        <PageContent></PageContent>
      </Space>
      <Footer />
    </div>
  );
}

export default App;
