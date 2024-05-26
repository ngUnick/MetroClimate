import { useState } from "react";
import { ApartmentOutlined } from "@ant-design/icons";
import { Badge, Card, Space } from "antd";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faTemperatureHalf,
  faDroplet,
} from "@fortawesome/free-solid-svg-icons";
import PropTypes from "prop-types";

function DashboardCard({ title, isTemp, value, online, showModal }) {
  const [isHover, setIsHover] = useState(false);

  const handleMouseEnter = () => {
     setIsHover(true);
  };
  const handleMouseLeave = () => {
     setIsHover(false);
  };

  // const boxStyle = {
     
  // };
  return (
    <Card style={{ margin: "20px", cursor: "pointer", boxShadow: isHover ? "0 4px 6px rgba(0,0,0,0.11)" : "0 2px 2px rgba(0,0,0,0.1)" }} onClick={showModal} onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}>
      <Space direction="vertical" style={{ padding: "10px" }}>
        <Space className="station-title">
          <ApartmentOutlined />
          {title}
          <Badge
            count={online ? "Online" : "Offline"}
            color={online ? "green" : "red"}
            offset={[10, 0]}
          />
        </Space>
        <Space className="station-info">
          <div>
            {value + (isTemp ? "°C" : "%")}
            <FontAwesomeIcon icon={isTemp ? faTemperatureHalf : faDroplet} size="xl" style={{marginLeft: "8px"}} color={isTemp ? "#e74c3c" : "#477ed6"} />
          </div>
          <div style={{ fontSize: "17px"}}>More info ➟</div>
        </Space>
      </Space>
    </Card>
  );
}

DashboardCard.propTypes = {
  title: PropTypes.string.isRequired,
  isTemp: PropTypes.bool.isRequired,
  value: PropTypes.number.isRequired,
  online: PropTypes.bool.isRequired,
  showModal: PropTypes.func.isRequired,
};

export default DashboardCard;