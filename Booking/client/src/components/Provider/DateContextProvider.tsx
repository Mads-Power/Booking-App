import {
  createContext,
  Dispatch,
  SetStateAction,
  useContext,
  useState,
} from "react";

export interface DateContextType {
  selectedDate: Date;
  setSelectedDate: Dispatch<SetStateAction<Date>>;
}

const DateContext = createContext<DateContextType>(undefined!);

export const DateProvider = ({ children }: any) => {
  const [selectedDate, setSelectedDate] = useState(new Date());

  return (
    <DateContext.Provider
      value={{ selectedDate: selectedDate, setSelectedDate: setSelectedDate }}
    >
      {children}
    </DateContext.Provider>
  );
};

export const useDateContext = () => useContext(DateContext);
