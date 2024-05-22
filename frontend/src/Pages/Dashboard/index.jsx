import { useState } from "react";
import { Space, Typography, Modal } from "antd";
import Graph from "../../Components/Graph";
import DashboardCard from "../../Components/DashboardCard";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faSatelliteDish,
} from "@fortawesome/free-solid-svg-icons";

function Dashboard() {
  const [open, setOpen] = useState(false);
  const showModal = () => {
    setOpen(true);
  };
  const handleOk = () => {
    setOpen(false);
  };
  const handleCancel = () => {
    setOpen(false);
  };


  const name = "Station 1"

  const data = [
    { year: '1991', value: 3 },
    { year: '1992', value: 4 },
    { year: '1993', value: 3.5 },
    { year: '1994', value: 5 },
    { year: '1995', value: 4.9 },
    { year: '1996', value: 6 },
    { year: '1997', value: 7 },
    { year: '1998', value: 9 },
    { year: '1999', value: 13 },
  ];


  const config = {
    data,
    height: 400,
    xField: 'year',
    yField: 'value',
  };


  return (
    <div>
      <Typography.Title level={1}>Dashboard</Typography.Title>
      <Typography.Title level={3}><FontAwesomeIcon icon={faSatelliteDish} style={{marginRight:"5px"}} /> Station: {name}</Typography.Title>
      <Space direction="horizontal">

        
        
        {/* This is the Card */}
        <DashboardCard
          title={"Sensor 1"}                                    // Station Name
          isTemp={true}
          value={12345}                                          // Value for Temperature                                            // Value for Humidity
          online={true}                                         // Boolean value, if Station is online the TRUE, otherwise False
          showModal={showModal}
        />


        {/* This is the Card */}
        <DashboardCard
          title={"Sensor 2"}                                    // Station Name
          isTemp={false}
          value={12345}                                          // Value for Temperature                                            // Value for Humidity
          online={true}                                         // Boolean value, if Station is online the TRUE, otherwise False
          showModal={showModal}
        />

        {/* <TemperatureGraph />
        <HumidityGraph /> */}
      </Space>



      {/* This is the Popup Window */}
      <Modal
        title={<Typography.Title level={2} style={{margin: "10px"}}>Sensor 1</Typography.Title>} 
        open={open}
        onOk={handleOk}
        onCancel={handleCancel}
        footer={(_, { OkBtn }) => (
          <>
            <OkBtn />
          </>
        )}
        okText={"Done"}
        width={"fit-content"}
      >
        <div style={{ display: "flex", flexDirection: "row", margin: "40px 20px 10px"}}>
          <Graph graphdata={data} graphconfig={config}/>
        </div>
      </Modal>
    </div>
  );
}




export default Dashboard;
