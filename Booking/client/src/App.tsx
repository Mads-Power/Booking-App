import { AppProvider } from '@components/Provider/app';
import Routers from '../src/routes/Routers';

function App() {
  return <AppProvider children={<Routers />} />;
}

export default App;
