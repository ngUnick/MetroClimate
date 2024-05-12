import {
  DollarCircleOutlined,
  ShoppingCartOutlined,
  ShoppingOutlined,
  UserOutlined,
} from "@ant-design/icons";
import { Card, Space, Statistic, Typography } from "antd";

function Dashboard() {
  return (
    <div>
      <Typography.Title level={4}>Dashboard</Typography.Title>
      <Space direction="horizontal">
        <DashboardCard
          icon={
            <ShoppingCartOutlined
              style={{
                color: "green",
                backgroundColor: "rgba(78, 232, 118, 0.25)",
                borderRadius: 20,
                fontSize: 24,
                padding: 8,
              }}
            />
          }
          title={"Orders"}
          value={12345}
        />
        <DashboardCard
          icon={<ShoppingOutlined style={{
            color: "purple",
            backgroundColor: "rgba(232, 78, 158, 0.25)",
            borderRadius: 20,
            fontSize: 24,
            padding: 8,
          }}/>}
          title={"Inventory"}
          value={12345}
        />
        <DashboardCard
          icon={<UserOutlined style={{
            color: "blue",
            backgroundColor: "rgba(78, 201, 232, 0.25)",
            borderRadius: 20,
            fontSize: 24,
            padding: 8,
          }}/>}
          title={"Customer"}
          value={12345}
        />
        <DashboardCard
          icon={<DollarCircleOutlined style={{
            color: "red",
            backgroundColor: "rgba(232, 78, 78, 0.25)",
            borderRadius: 20,
            fontSize: 24,
            padding: 8,
          }}/>}
          title={"Revenue"}
          value={12345}
        />
      </Space>
    </div>
  );
}

function DashboardCard({ title, value, icon }) {
  return (
    <Card>
      <Space direction="horizontal">
        {icon}
        <Statistic title={title} value={value} />
      </Space>
    </Card>
  );
}

export default Dashboard;
