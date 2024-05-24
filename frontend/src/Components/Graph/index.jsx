import { Line } from '@ant-design/charts';

const Graph = ({graphdata, graphconfig}) => {
  const data = graphdata;

  const config = graphconfig;

  return <Line {...config} />;
};

export default Graph;
