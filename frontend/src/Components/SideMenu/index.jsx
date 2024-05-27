import { AppstoreOutlined, SettingOutlined } from "@ant-design/icons";
import { Menu } from "antd";
import { useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faSatelliteDish,
} from "@fortawesome/free-solid-svg-icons";

function SideMenu() {
  const navigate = useNavigate()
  return (
    <div className="side-menu">
      <Menu style={{fontSize: "18px", marginTop: "10px", marginLeft: "5px"}}
        onClick={(item) => {
          navigate(item.key);
        }}
        items={[
          {
            label: "Dashboard",
            key: "/",
            icon:<AppstoreOutlined style={{fontSize: "18px"}}/>,
          },
          {
            label: "Stations",
            key: "/stations",
            icon:<FontAwesomeIcon icon={faSatelliteDish} style={{fontSize: "18px"}}/>,
          },
          {
            label: "Settings",
            key: "/settings",
            icon:<SettingOutlined style={{fontSize: "18px"}}/>,
          },
        ]}
      ></Menu>
    </div>
  );
}

export default SideMenu;
