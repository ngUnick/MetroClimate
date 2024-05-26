import { useState } from "react";
import { Badge, Card, Space } from "antd";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSatelliteDish } from "@fortawesome/free-solid-svg-icons";
import PropTypes from "prop-types";

function StationCard({ title, description, online}) {
  const [isHover] = useState(false);

  // const handleMouseEnter = () => {
  //   setIsHover(true);
  // };
  // const handleMouseLeave = () => {
  //   setIsHover(false);
  // };

  // const boxStyle = {};
  return (
    <Card
      style={{
        margin: "20px",
        /*cursor: "pointer",*/ maxWidth: "500px", height: "260px",
        boxShadow: isHover ? "0 4px 6px rgba(0,0,0,0.11)" : "0 2px 2px rgba(0,0,0,0.1)",
      }} /*onClick={showModal} onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave}*/
    >
      <Space direction="vertical" style={{ padding: "10px" }}>
        <Space className="station-title">
          <FontAwesomeIcon icon={faSatelliteDish} />
          {title}
          <Badge
            count={online ? "Online" : "Offline"}
            color={online ? "green" : "red"}
            offset={[10, 0]}
          />
        </Space>
        <Space className="station-info" direction="vertical">
          <div
            style={{
              maxHeight: "500px",
              display: "-webkit-box",
              WebkitBoxOrient: "vertical",
              WebkitLineClamp: "3",
              textOverflow: "ellipsis",
              overflow: "hidden",
            }}
          >
            {description}
          </div>
          {/* <div style={{ fontSize: "17px", marginTop: "10px" }}>More info ➟</div> */}
        </Space>
      </Space>
    </Card>
  );
}

StationCard.propTypes = {
  title: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  online: PropTypes.bool.isRequired,
};

export default StationCard;
