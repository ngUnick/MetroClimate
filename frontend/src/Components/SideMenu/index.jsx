import { AppstoreOutlined, ShopOutlined, ShoppingCartOutlined, UserOutlined } from "@ant-design/icons";
import { Menu } from "antd";

function SideMenu() {
  return (
    <div className="side-menu">
      <Menu
        onClick={(item) => {}}
        items={[
          {
            label: "Dashboard",
            key: "/",
            icon:<AppstoreOutlined />,
          },
          {
            label: "Inventory",
            key: "/inventory",
            icon:<ShopOutlined />,
          },
          {
            label: "Orders",
            key: "/orders",
            icon:<ShoppingCartOutlined />,
          },
          {
            label: "Customers",
            key: "/customers",
            icon:<UserOutlined />,
          },
        ]}
      ></Menu>
    </div>
  );
}

export default SideMenu;
