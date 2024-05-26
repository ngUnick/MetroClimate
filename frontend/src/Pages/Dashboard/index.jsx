import { useEffect,useState } from "react";
import { Space, Typography, Modal } from "antd";
import Graph from "../../Components/Graph";
import DashboardCard from "../../Components/DashboardCard";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faSatelliteDish,
} from "@fortawesome/free-solid-svg-icons";
import apiService from "../../ApiService";
import { DateTime } from "luxon";

function Dashboard() {
  const [open, setOpen] = useState(false);

  const [stations, setStations] = useState([]);

  const [sensorName, setSensorName] = useState(null);
  const [config, setConfig] = useState({});

  // const dataO = [
  //   { time: '1991', value: 3 },
  //   { time: '1992', value: 4 },
  //   { time: '1993', value: 3.5 },
  //   { time: '1994', value: 5 },
  //   { time: '1995', value: 4.9 },
  //   { time: '1996', value: 6 },
  //   { time: '1997', value: 7 },
  //   { time: '1998', value: 9 },
  //   { time: '1999', value: 13 },
  // ];


  const showModal = (name, id) => () => {
    setSensorName(name);
    setOpen(true);
   
    fetchSensorData(id).then((data) => {
      //group data by hour and get average value and add time to it
      const processedData = processSensorData(data);
      const processedDataConfig  = {
        data: processedData,
        height: 400,
        xField: 'time',
        yField: 'value',
        xAxis: {
          type: 'time',
          title: {
            text: 'Time',
          },
          label: {
            rotate: 0, // Rotate labels to be horizontal
            formatter: (v) => DateTime.fromMillis(v).toFormat('HH:mm'),
          },
        },
      };
      setConfig(processedDataConfig);
    
        
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

  const processSensorData = (data) => {
    const timezone = 'Europe/Athens';
    return data.map((item) => {
      const time = DateTime.fromISO(item.created).setZone(timezone).toFormat('HH:mm');
      return { time: time, value: item.value};
    });
  };

  


  return (
    <div>
      <Typography.Title level={1}>Dashboard</Typography.Title>
      {stations.map((station) => {
        const name = station.name; // Add this line to set name from station.name
        return (
          <div key={station.id}>
            {station.sensors && station.sensors.length > 0 && (
              <>
                <Typography.Title level={3}>
                  <FontAwesomeIcon
                    icon={faSatelliteDish}
                    style={{ marginRight: "5px" }}
                  />{" "}
                  Station: {name}
                </Typography.Title>
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
              </>
            )}
          </div>
        );
      })}
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
          <Graph graphconfig={config}/>
        </div>
      </Modal>
    </div>
  );
}




export default Dashboard;
