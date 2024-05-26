import { useEffect, useState } from "react";
import { Typography, Modal, Card, Form, Input, InputNumber } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import StationCard from "../../Components/StationCard";
import apiService from "../../ApiService";

function Stations() {
  const [open, setOpen] = useState(false);
  const [stations, setStations] = useState([]);

  const showModal = () => {
    setOpen(true);
  };
  const handleOk = () => {
    setOpen(false);
  };
  const handleCancel = () => {
    setOpen(false);
  };

  const [isHover, setIsHover] = useState(false);

  const handleMouseEnter = () => {
    setIsHover(true);
  };
  const handleMouseLeave = () => {
    setIsHover(false);
  };


  const onFinish = (values) => {
    console.log('Success:', values);
  };
  const onFinishFailed = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };

  const fetchStations = async () => {
    const response = await apiService.get("/Station");
    setStations(response.data.data);
  };

  useEffect(() => {
    fetchStations();
  }, []);



  const description =
    "N/A";

  return (
    <div>
      <Typography.Title level={1}>Stations</Typography.Title>
      <div style={{ display: "flex", flexDirection: "row" }}>
        
        {stations.map((station) => (
          <StationCard
            key={station.id}
            title={station.name}
            online={station.online}
            description={station.description || description}
            // showModal={showModal}
          />
        ))}
        
        <Card
          style={{
            width: "500px",
            height: "260px",
            margin: "20px",
            boxShadow: isHover
              ? "0 4px 6px rgba(0,0,0,0.11)"
              : "0 2px 2px rgba(0,0,0,0.1)",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            cursor: "pointer",
          }}
          onMouseEnter={handleMouseEnter}
          onMouseLeave={handleMouseLeave}
          onClick={showModal}
        >
          <Typography.Title
            level={3}
            style={{
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              alignItems: "center",
              color: "#707070",
            }}
          >
            <PlusOutlined
              style={{ fontSize: "60px", margin: "-15px 0px 15px" }}
            />
            Add New Station
          </Typography.Title>
        </Card>
        



        
        {/* This is the Popup Window */}
        <Modal
          title={
            <Typography.Title level={2} style={{ margin: "10px" }}>
              Add New Station
            </Typography.Title>
          }
          open={open}
          onOk={handleOk}
          onCancel={handleCancel}
          footer={(_, { OkBtn, CancelBtn  }) => (
            <>
            <CancelBtn />
              <OkBtn type="primary" htmlType="submit" />
            </>
          )}
          okText={"Done"}
          width={"fit-content"}
        >
          <div
            style={{
              display: "flex",
              flexDirection: "row",
              margin: "40px 20px 10px",
              maxWidth: "500px",
              fontSize: "20px",
            }}
          >
            <Form
              name="basic"
              labelCol={{
                span: 8,
              }}
              wrapperCol={{
                span: 16,
              }}
              style={{
                maxWidth: 600,
                margin: "20px"
              }}
              initialValues={{
                remember: true,
              }}
              onFinish={onFinish}
              onFinishFailed={onFinishFailed}
              autoComplete="off"
            >
                <Form.Item
                label="ID"
                name="id"
                rules={[
                  {
                    required: true,
                    message: "Please input a name!",
                  },
                ]}
              >
                <InputNumber min={1} max={10} defaultValue={1} />
              </Form.Item>
                
              <Form.Item
                label="Name"
                name="name"
                rules={[
                  {
                    required: true,
                    message: "Please input a name!",
                  },
                ]}
              >
                <Input />
              </Form.Item>

              <Form.Item
                label="Description"
                name="description"
                rules={[
                  {
                    required: true,
                    message: "Please input your password!",
                  },
                ]}
                style={{marginLeft: "-6px"}}
              >
                <Input.TextArea style={{marginLeft: "5px"}}/>
              </Form.Item>
            </Form>
          </div>
        </Modal>
      </div>
    </div>
  );
}

export default Stations;
