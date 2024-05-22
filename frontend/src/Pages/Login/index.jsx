import React, { useEffect, useContext } from "react";
import { LockOutlined, UserOutlined } from "@ant-design/icons";
import { Button, Checkbox, Form, Input, message, Typography } from "antd";
import { useNavigate } from "react-router-dom";


const Login = () => {
    const navigate = useNavigate();
    const [messageApi, contextHolder] = message.useMessage();
    // const { login } = useContext(AuthContext);
  
    useEffect(() => {
      console.log('mpike');
    },[])
  
    const onFinish = async (values) => {
      console.log("Received values of form: ", values);
    //   try {
    //     const {username , password} = values;
    //     const token = await AuthAPI.login(username, password);
    //     messageApi.open({
    //       type: 'success',
    //       content: 'Successfully login',
    //     });
    //     console.log(token);
    //     login(token);
    //     navigate("/")
    //   } catch (error) {
    //     messageApi.open({
    //       type: 'error',
    //       content: 'Wrong username or password',
    //     });
    //     console.log("lathos onoma h kodikos");
    //   }
    };
  
    return (
      <div style={{height: "fit-content", width: "fit-content", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center"}}>
        <Typography.Title style={{marginBottom: "40px"}}>Login</Typography.Title>
          <Form
          style={{width: '250px', height: '500px', margin: 'auto'}}
            name="normal_login"
            className="login-form"
            initialValues={{
              remember: true,
            }}
            onFinish={onFinish}
          >
            {contextHolder}
            <Form.Item
              name="username"
              rules={[
                {
                  required: true,
                  message: "Please input your Username!",
                },
              ]}
            >
              <Input
                prefix={<UserOutlined className="site-form-item-icon" />}
                placeholder="Username"
              />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[
                {
                  required: true,
                  message: "Please input your Password!",
                },
              ]}
            >
              <Input
                prefix={<LockOutlined className="site-form-item-icon" />}
                type="password"
                placeholder="Password"
              />
            </Form.Item>
            <Form.Item>
              <Form.Item name="remember" valuePropName="checked" noStyle>
                <Checkbox>Remember me</Checkbox>
              </Form.Item>
              <span style={{ marginRight: "8px" }}>|</span>
              Or <a href="/Register">Register now!</a>
            </Form.Item>
            <Form.Item style={{display: "flex", flexDirection: "column", alignItems: "center"}}>
              <Button
                type="primary"
                htmlType="submit"
                className="login-form-button"
                style={{ marginRight: "5px" }}
              >
                Log in
              </Button>
            </Form.Item>
          
            <br />
            <br />
            {/* <Button type="primary" value="large" onClick={() => navigate('/Upload')}>Go to Upload</Button> */}
          </Form>
      </div>
    );
  };
  export default Login;