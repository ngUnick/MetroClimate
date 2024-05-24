import { Typography, Button } from "antd";

function Settings() {
  const handleLogout = () => {
    console.log("User logged out");
  };

  return (
    <div>
      <Typography.Title level={4}>Settings</Typography.Title>
      <Button type="primary" danger onClick={handleLogout} style={{marginTop: "10px"}}>
        Logout
      </Button>
    </div>
  );
}

export default Settings;
