import { userAtom } from '@components/Provider/jotaiProvider';
import { useQuery } from '@tanstack/react-query';
import { Booking } from '@type/booking';
import { User } from '@type/user';
import { atom, useAtom } from 'jotai'
import { atomsWithQuery } from 'jotai-tanstack-query'
import { getMe } from './getMe';
import { getMyBookings } from './getMyBookings';


const url = `/api/Account/IsAuthenticated`;
export const getIsAuthenticated = async () => {

  // const [user, setUser] = useAtom(userAtom);
  let isSuccessful = false;
  const requestOptions = {
    method: 'GET',
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
  };
  const res = await fetch(url, requestOptions).then(async auth => {
    if (auth.ok) {
      await getMe().then(async me => {
        if (me.ok) {
          // let loggedInUser: Partial<User> = me.json() as Partial<User>;
          console.log(me.json());
          // getMyBookings().then(bookings => {
          //   loggedInUser.bookings = bookings.json() as unknown as Booking[];
          //   isSuccessful = true;
          // });
          // console.log(loggedInUser)
        }
      })
    }
  });
  return isSuccessful;
};

export const useIsAuthenticated = () => {
  return useQuery({
    queryKey: ['isAuthenticated'],
    queryFn: () => getIsAuthenticated()
  });
};