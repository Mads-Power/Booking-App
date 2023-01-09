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
  const initialValue = new Date(new Date().setHours(0,0,0,0));
  const [selectedDate, setSelectedDate] = useState(initialValue);

  return (
    <DateContext.Provider
      value={{ selectedDate: selectedDate, setSelectedDate: setSelectedDate }}
    >
      {children}
    </DateContext.Provider>
  );
};

export const useDateContext = () => useContext(DateContext);
