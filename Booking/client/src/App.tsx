import { AppProvider } from "@components/Provider/app";
import { DateProvider } from "@components/Provider/DateContextProvider";
import { UserProvider } from "@components/Provider/UserContextProvider";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import Routers from "../src/routes/Routers";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <DateProvider>
        <UserProvider>
          <AppProvider children={<Routers />} />
        </UserProvider>
      </DateProvider>
    </QueryClientProvider>
  );
}

export default App;
