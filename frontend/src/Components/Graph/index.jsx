import { Line } from '@ant-design/charts';
import PropTypes from 'prop-types';

const Graph = ({graphconfig}) => {

  const config = graphconfig;

  return <Line {...config} />;
};

Graph.propTypes = {
  graphconfig: PropTypes.object.isRequired,
};

export default Graph;
