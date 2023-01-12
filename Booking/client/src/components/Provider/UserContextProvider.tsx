import { useUser } from "@api/getUser";
import { QueryObserverResult } from "@tanstack/react-query";
import { User } from "@type/user";
import {
  createContext,
  Dispatch,
  SetStateAction,
  useContext,
  useEffect,
  useState,
} from "react";

export interface UserContextType {
  user: User;
  setUser: Dispatch<SetStateAction<User>>;
  addBooking(b: any): void;
  removeBooking(b: any): void;
}

const defaultUser: User = 
{
  id: 5,
  name: "Default User",
  email: "default@user.com",
  phoneNumber: "01234567",
  bookings: [
    {
      "id": 24,
      "seatId": 5,
      "userId": 5,
      "date": "2023-01-03T12:00:00+01:00"
    }
  ]
}

const handleInitialUser = (apiUser: any): User => {
  if (apiUser) {
    return apiUser;
  }
  else {
    return defaultUser;
  }
}

const UserContext = createContext<UserContextType>({user: defaultUser, setUser: undefined!, addBooking: undefined!, removeBooking: undefined!});

export const UserProvider = ({ children }: any) => {
  const apiUser = useUser("5").data;
  const [user, setUser] = useState<User>(defaultUser);

  const addBooking = (booking: any) => {
    user.bookings.push(booking)
  }

  const removeBooking = (booking: any) => {
    const index = user.bookings.indexOf(booking);
    if (index >= 0) {
      user.bookings.splice(index, 1)
    }
  }

  useEffect(() => {
    setUser(handleInitialUser(apiUser))
  }, [apiUser, user.bookings]);

  return (
    <UserContext.Provider
      value={{ user: user, setUser: setUser, addBooking: addBooking, removeBooking: removeBooking}}
    >
      {children}
    </UserContext.Provider>
  );
};

export const useUserContext = () => useContext(UserContext);
