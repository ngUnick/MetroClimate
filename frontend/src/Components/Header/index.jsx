import { Badge, Image, Space, Typography } from "antd";
import { MailOutlined, BellFilled } from "@ant-design/icons";

function Header() {
  return (
    <div className="header">
      <Image
        width={40}
        src="https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/React-icon.svg/2300px-React-icon.svg.png"
      ></Image>
      <Typography.Title>Dashboard</Typography.Title>
      <Space>
        <Badge count={10} dot>
          <MailOutlined style={{ fontSize: 24 }} />
        </Badge>

        <Badge count={20}>
          <BellFilled style={{ fontSize: 24 }} />
        </Badge>
      </Space>
    </div>
  );
}

export default Header;
