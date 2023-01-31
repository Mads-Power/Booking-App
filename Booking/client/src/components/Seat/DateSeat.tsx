import { SetStateAction, Dispatch, useState } from 'react';
import { TextField } from '@mui/material';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import {
  LocalizationProvider,
  PickersDay,
  PickersDayProps,
  StaticDatePicker,
  pickersDayClasses,
} from '@mui/x-date-pickers';
import dayjs from 'dayjs';
import { useAtom } from 'jotai';
import { dateAtom } from '../Provider/app';
import { Seat } from '@type/seat';
import { Booking } from '@type/booking';
import { User } from '@type/user';

type TDateSeat = {
  data: Seat;
  userData: User;
  onSeatInfoChange: Dispatch<SetStateAction<string>>;
};

export const DateSeat = ({
  data,
  userData,
  onSeatInfoChange,
}: TDateSeat) => {
  const [date, setDate] = useAtom(dateAtom);
  const getOccupiedDays = (bookings: Booking[], date: Date) => {
    const dateDay = dayjs(date);

    const occupiedDaysInMonth = bookings.map((booking: Booking) => {
      if (
        dayjs(booking.date).month() === dateDay.month() &&
        dayjs(booking.date).year() === dateDay.year()
      )
        return { date: dayjs(booking.date).date(), email: booking.email };
    });

    return occupiedDaysInMonth;
  };
  const occupiedDays = getOccupiedDays(data?.bookings, date);

  const handleOccupiedDays = (
    day: Date,
    selectedDate: Date[],
    pickerDayProps: PickersDayProps<Date>
  ) => {
    const dayDate = dayjs(day);
    const isOccupied = occupiedDays?.find(day => day?.date === dayDate.date());
    const isOccupiedByCurrentUser = occupiedDays?.find(day => day?.date === dayDate.date() && day.email === userData.email);

    // Blue fill + border
    const deskBookedByOtherUserStyle = {
      border: "2px solid #3981F1",
      background: "rgba(57, 129, 241, 0.25)"
    } as const

    // Green fill + border
    const deskBookedByCurrentUserStyle = {
      border: "2px solid #61C577",
      background: "rgba(97, 197, 119, 0.25)"
    } as const

    // No fill + no border
    const deskNotOccupiedStyle = {
      color: "black"
    } as const

    //When selected, orange fill + border
    const deskNotOccupiedOnSelectedStyle = {
      border: "2px solid #F68420",
      background: "rgba(246, 132, 32, 0.25)",
      color: "black"
    } as const


    // Ref for conditional styling for mui elements:
    // https://stackoverflow.com/questions/69500357/how-to-implement-conditional-styles-in-mui-v5-sx-prop

    return (
      <PickersDay
        sx={{
          ...(isOccupied && !isOccupiedByCurrentUser && deskBookedByOtherUserStyle),
          ...(isOccupiedByCurrentUser && deskBookedByCurrentUserStyle),
          ...(!isOccupied && !isOccupiedByCurrentUser && deskNotOccupiedStyle),
          [`&&.${pickersDayClasses.selected}`]: (
            deskNotOccupiedOnSelectedStyle
          )
        }}
        {...pickerDayProps}
      />
    );
  };

  const handleMonthChange = (date: Date) => {
    const today =  new Date(new Date().setHours(0,0,0,0));
    date = new Date(date.toISOString())
    if (date.getMonth() === today.getMonth()) {
      setDate(new Date(today.toISOString()));
    }
    else {
      setDate(new Date(date));
    }
  };

  const handleChange = (newValue: Date) => {
    const dayDate = dayjs(newValue);
    const correctedDate = new Date(newValue.toISOString());
    let occupied = false;
    occupiedDays?.find(day => {
      const selectedDateIsToday = day?.date === dayDate.date();
      const selectedDateIsBookedByCurrentUser = day?.email === userData.email

      if (selectedDateIsToday && selectedDateIsBookedByCurrentUser) {
        onSeatInfoChange('removeBookedSeat');
        occupied = true
      } 
      if (selectedDateIsToday && !selectedDateIsBookedByCurrentUser) {
        onSeatInfoChange('bookedSeat');
        occupied = true
      }
    });

    if (!occupied) {
      onSeatInfoChange('bookAvailableSeat');
    }

    setDate(new Date(correctedDate));
  };

  // userID = your or Someone else
  // undefined = default
  return (
    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale='nb'>
      <StaticDatePicker
        value={date}
        onChange={date => handleChange(date!)}
        onMonthChange={handleMonthChange}
        renderInput={params => <TextField {...params} />}
        renderDay={handleOccupiedDays}
        closeOnSelect={false}
        showToolbar={false}
        disablePast={true}
        displayStaticWrapperAs='desktop'
        openTo='day'
      />
    </LocalizationProvider>
  );
};
