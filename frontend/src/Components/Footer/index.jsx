import { Typography } from "antd";

function Footer() {
  return (
    <div className="footer">
      <Typography.Link>Amog</Typography.Link>
      <Typography.Link href="https://www.google.com" target={"_blank"}>
        Privacy Policy
      </Typography.Link>
      <Typography.Link href="https://www.google.com" target={"_blank"}>
        Terms of Use
      </Typography.Link>
    </div>
  );
}

export default Footer;
