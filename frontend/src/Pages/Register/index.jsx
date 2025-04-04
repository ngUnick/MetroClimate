import { Form, Input, Button, message, Typography } from 'antd';
import { useNavigate } from "react-router-dom";
import apiService from '../../ApiService';

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 8 },
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 16 },
  },
};

const tailFormItemLayout = {
  wrapperCol: {
    xs: { span: 24, offset: 0 },
    sm: { span: 16, offset: 8 },
  },
};

const Register = () => {
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();
  const [form] = Form.useForm();

  const onFinish = async (values) => {
    console.log('Received values of form: ', values);
    try {

      const response = await apiService.register(values.username, values.email, values.password, values.confirm);
      messageApi.open({
        type: 'success',
        content: 'Successfully registered',
      });
      console.log(response.data.token);
      navigate("/");
    } catch (error) {
      //check if error is 400
      if (error.response.status === 400) {
        const { validationErrors } = error.response.data;
        form.setFields(Object.keys(validationErrors).map(key => ({
          name: key,
          errors: validationErrors[key],
        })));
      } else {
        messageApi.open({
          type: 'error',
          content: 'There was an error while registering',
        });
      }
    }
  };

  return (
    <div style={{height: "fit-content", width: "fit-content", display: "flex", flexDirection: "column", justifyContent: "center", alignItems: "center"}}>
        <Typography.Title style={{ marginBottom: "40px", marginLeft: "55px" }} >Register</Typography.Title>
        <Form
          {...formItemLayout}
          form={form}
          name="register"
          onFinish={onFinish}
          initialValues={{
            prefix: '86',
          }}
          style={{
            width: '400px', height: '500px',
          }}
          scrollToFirstError
        >
          {contextHolder}
          <Form.Item
            name="username"
            label="Username"
            rules={[
              {
                required: true,
                message: 'Please input your username!',
                whitespace: true,
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="email"
            label="E-mail"
            rules={[
              {
                required: true,
                message: 'Please input your E-mail!',
                whitespace: true,
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="password"
            label="Password"
            rules={[
              {
                required: true,
                message: 'Please input your password!',
              },
            ]}
            hasFeedback
          >
            <Input.Password />
          </Form.Item>
          <Form.Item
            name="confirm"
            label="Confirm Password"
            dependencies={['password']}
            hasFeedback
            rules={[
              {
                required: true,
                message: 'Please confirm your password!',
              },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('The passwords do not match!'));
                },
              }),
            ]}
          >
            <Input.Password />
          </Form.Item>
          <div style={{display: "flex", justifyContent: "flex-end"}}>
          Or <a href="/" style={{marginLeft: "5px"}}>Login now!</a>
          </div>
          <Form.Item {...tailFormItemLayout} style={{display: "flex", justifyContent: "center"}}>
            <Button type="primary" htmlType="submit">
              Register
            </Button>
          </Form.Item>
        </Form>
    </div>
  );
};

export default Register;