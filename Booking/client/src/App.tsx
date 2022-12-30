import { AppProvider } from "@components/Provider/app";
import { DateProvider } from "@components/Provider/DateContextProvider";
import Routers from "../src/routes/Routers";

function App() {
  return (
    <DateProvider>
      <AppProvider children={<Routers />} />
    </DateProvider>
  );
}

export default App;
