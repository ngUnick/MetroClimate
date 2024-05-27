import { useState } from "react";
import { ApartmentOutlined } from "@ant-design/icons";
import { Badge, Card, Space } from "antd";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faTemperatureHalf,
  faDroplet,
} from "@fortawesome/free-solid-svg-icons";
import PropTypes from "prop-types";

function DashboardCard({ title, typeEnum, symbol, value, online, showModal }) {
  const [isHover, setIsHover] = useState(false);


  const handleMouseEnter = () => {
      
     setIsHover(true);
  };
  const handleMouseLeave = () => {
     setIsHover(false);
  };
  const typeProperties = [
    {
      id : 0,
      name: 'temperature',
      icon: faTemperatureHalf,
      color: "#e74c3c"
    },
    {
      id : 1,
      name: 'humidity',
      icon: faDroplet,
      color: "#477ed6"
    },
    // Add other mappings as needed
  ]

  const [typeKey] = useState(typeProperties.find((type) => type.id === typeEnum));

  // const boxStyle = {
     
  // };
  return (
    <Card
      style={{
        margin: "20px",
        cursor: "pointer",
        boxShadow: isHover
          ? "0 4px 6px rgba(0,0,0,0.11)"
          : "0 2px 2px rgba(0,0,0,0.1)",
      }}
      onClick={showModal}
      onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
    >
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
            {value + (symbol || "")}
            <FontAwesomeIcon
              icon= {typeKey.icon}
              size="xl"
              style={{ marginLeft: "8px" }}
              color={typeKey.color}
            />
          </div>
          <div style={{ fontSize: "17px" }}>More info ➟</div>
        </Space>
      </Space>
    </Card>
  );
}

DashboardCard.propTypes = {
  title: PropTypes.string.isRequired,
  typeEnum: PropTypes.number.isRequired,
  symbol: PropTypes.string,
  value: PropTypes.number.isRequired,
  online: PropTypes.bool.isRequired,
  showModal: PropTypes.func.isRequired,
};

export default DashboardCard;