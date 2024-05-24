import { Badge, Image, Space, Typography, Avatar } from "antd";
import { UserOutlined, BellFilled } from "@ant-design/icons";
import logo from "../../assets/original_logo.png"

function Header() {
  const name = "Nick";
  return (
    <div className="header">
      <Image
        width={130}
        src={logo}
        preview={false}
      ></Image>
      <Typography.Title style={{textTransform: "capitalize"}}>Welcome {name}!</Typography.Title>
      <Space>
        <Badge dot color="green" size="large" offset={[-34, 6]}>
          <Avatar size={34} icon={<UserOutlined />} src="https://api.dicebear.com/7.x/miniavs/svg?seed=1" style={{border: "1px green solid", padding: "", width: "40px", height: "40px"}}/>
        </Badge>
      </Space>
    </div>
  );
}

export default Header;
