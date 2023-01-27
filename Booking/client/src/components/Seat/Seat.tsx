import { useEffect, useState } from 'react';
import { useSeatQuery } from '@api/useSeatQuery';
import { CircularProgress } from '@mui/material';
import { useParams, useNavigate } from 'react-router-dom';
import {
  nbNO,
} from '@mui/x-date-pickers';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import dayjs from 'dayjs';
import 'dayjs/locale/nb';
import { nbNO as coreNbNO } from '@mui/material/locale';
import { BookSeat } from './BookSeat';
import { DateSeat } from './DateSeat';
import ArrowCircleLeftIcon from '@mui/icons-material/ArrowCircleLeft';
import { useAtom } from 'jotai';
import { dateAtom } from '../Provider/jotaiProvider';
import { useUserQuery } from '@api/useUserQuery';
import { ColorDescription } from '@components/ColorDescription/colorDescription';
import { Divider } from '@mui/material'

const theme = createTheme(
  {
  },
  nbNO, // x-date-pickers translations
  coreNbNO // core translations
);

export const Seat = () => {
  const { seatId } = useParams();
  const { isLoading, data, error } = useSeatQuery(seatId!);
  // const [user, setUser] = useAtom(userAtom);
  const { data: userData } = useUserQuery('emil.onsoyen@itverket.no');
  const [date] = useAtom(dateAtom);
  const navigate = useNavigate();

  const initialSeatState = () => {
    if (!userData) return "";
    let state = "";
    data?.bookings.some(booking => {
      const dateIsoString = new Date(booking.date).toLocaleString();
      if (dateIsoString === date.toLocaleString()) {
        if (booking.email === userData?.email) {
          state = 'removeBookedSeat';
          return true;
        } else if (booking.email !== userData?.email) {
          state = 'bookedSeat';
          return true;
        }
      }
    });
    if (!state.length) {
      state = 'bookAvailableSeat';
    }
    return state;
  }

  const [seatInfo, setSeatInfo] = useState(initialSeatState);

  useEffect(() => {
    if (!initialSeatState.length) {
      setSeatInfo(initialSeatState());
    }

  }, [data, userData]);

  if (isLoading) {
    return (
      <div style={{ display: 'flex' }}>
        <CircularProgress size={100} style={{ margin: '10vh auto' }} />
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke hente setet.</h4>;
  }


  const dayDate = dayjs(date);
  return (
    <>
      <ThemeProvider theme={theme}>
        <div className='flex flex-col gap-y-8 md:h-full'>
          <div className="p-2 flex flex-row align-baseline hover:cursor-pointer" onClick={() => navigate(`/`)}>
            <ArrowCircleLeftIcon
              htmlColor='#DF8B0D'
            />
            <p>Tilbake</p>
          </div>
          <div className='flex flex-col gap-y-4 w-[90%] mx-auto md:flex-row md:h-[60%]'>
            <div className='flex flex-col md:basis-3/4'>
              <div className='m-3'>
                <ColorDescription />
              </div>
              <div>
                <DateSeat
                  data={data!}
                  userData={userData!}
                  onSeatInfoChange={setSeatInfo}
                />
              </div>
            </div>
            <Divider className="mx-2 bg-black md:hidden" />
            <div className='flex flex-col w-full'>
              <BookSeat
                seat={data!}
                date={dayDate}
                data={userData!}
                seatInfo={seatInfo}
                onSeatInfoChange={setSeatInfo}
              />
            </div>
          </div>
        </div>
      </ThemeProvider>
    </>
  );
};
