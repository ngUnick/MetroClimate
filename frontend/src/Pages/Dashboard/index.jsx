import { useEffect,useState } from "react";
import { Space, Typography, Modal } from "antd";
import Graph from "../../Components/Graph";
import DashboardCard from "../../Components/DashboardCard";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faSatelliteDish,
} from "@fortawesome/free-solid-svg-icons";
import apiService from "../../ApiService";

function Dashboard() {
  const [open, setOpen] = useState(false);

  const [stations, setStations] = useState([]);

  const [sensorId, setSensorId] = useState(null);
  const [sensorName, setSensorName] = useState(null);
  const [sensorData, setSensorData] = useState(null);


  const showModal = (name, id) => () => {
    setSensorId(id);
    setSensorName(name);
    setOpen(true);
    fetchSensorData(id).then((data) => {
      setSensorData(data);
    });
  };
  const handleOk = () => {
    setOpen(false);
  };
  const handleCancel = () => {
    setOpen(false);
  };

  const fetchStations = async () => {
    const response = await apiService.get("/Station");
    setStations(response.data.data);
  };

  useEffect(() => {
    // Fetch data immediately when component mounts
    fetchStations();

    // Set up interval to fetch data every 10 seconds
    const intervalId = setInterval(fetchStations, 10000); // 10000 ms = 10 seconds

    // Clear interval on component unmount
    return () => clearInterval(intervalId);
  }, []);

  const fetchSensorData = async (sensorId) => {
    const response = await apiService.get("/Reading", {sensorId});
    return response.data.data;
  }



  // const name = "Station 1"

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
      {stations.map((station) => {
        const name = station.name; // Add this line to set name from station.name
        return (
          <div key={station.id}>
            
            <Typography.Title level={3}><FontAwesomeIcon icon={faSatelliteDish} style={{marginRight:"5px"}} /> Station: {name}</Typography.Title>
            <Space direction="horizontal">
              {station.sensors.map((sensor) => {
                return (
                  <DashboardCard
                    key={sensor.id}
                    title={sensor.name}                                 
                    typeEnum={sensor.type}
                    symbol={sensor.symbol}                         
                    value={sensor.lastReading}                                     
                    online={sensor.online}      
                    showModal={showModal(sensor.name, sensor.id)}
                  />
                );
              })}
            </Space>
            {/* This is the Popup Window */}
            <Modal
              title={<Typography.Title level={2} style={{margin: "10px"}}>{sensorName}</Typography.Title>}
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
      })}
    </div>
  );
}




export default Dashboard;
