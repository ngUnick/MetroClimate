import { Typography, Button } from "antd";
import apiService from "../../ApiService";

function Settings() {
  const handleLogout = () => {
    apiService.logout();
    window.location.reload();
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
