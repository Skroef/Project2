import mock from '@/fake-db/mock.js'

const data = {
  dispatchedOrders: [
    {
      orderNo: 1,
      status: 'Active',
      statusColor: 'success',
      operator: 'Cinar Knowles',
      email: 'cinarknowles@gmail.com',
      operatorImg: require('@/assets/images/portrait/small/avatar-s-2.jpg'),
      usersLiked: [
        {
          name: 'Vennie Mostowy',
          img: require('@/assets/images/portrait/small/avatar-s-5.jpg')
        },
        {
          name: 'Elicia Rieske',
          img: require('@/assets/images/portrait/small/avatar-s-7.jpg')
        },
        {
          name: 'Julee Rossignol',
          img: require('@/assets/images/portrait/small/avatar-s-10.jpg')
        },
        {
          name: 'Darcey Nooner',
          img: require('@/assets/images/portrait/small/avatar-s-8.jpg')
        }
      ],
      location: 'Anniston, Alabama',
      distance: '130 km',
      distPercent: 80,
      startDate: '26/07/2018',
      estDelDate: '28/07/2018'
    },
    {
      orderNo: 2,
      status: 'Do Not Disturb',
      statusColor: 'danger',
      operator: 'Britany Ryder',
      email: 'britanyryder@gmail.com',
      operatorImg: require('@/assets/images/portrait/small/avatar-s-4.jpg'),
      usersLiked: [
        {
          name: 'Trina Lynes',
          img: require('@/assets/images/portrait/small/avatar-s-1.jpg')
        },
        {
          name: 'Lilian Nenez',
          img: require('@/assets/images/portrait/small/avatar-s-2.jpg')
        },
        {
          name: 'Alberto Glotzbach',
          img: require('@/assets/images/portrait/small/avatar-s-3.jpg')
        }
      ],
      location: 'Cordova, Alaska',
      distance: '234 km',
      distPercent: 60,
      startDate: '26/07/2018',
      estDelDate: '28/07/2018'
    },
    {
      orderNo: 3,
      status: 'Away',
      statusColor: 'warning',
      operator: 'Kishan Ashton',
      email: 'kishanashton@gmail.com',
      operatorImg: require('@/assets/images/portrait/small/avatar-s-1.jpg'),
      usersLiked: [
        {
          name: 'Lai Lewandowski',
          img: require('@/assets/images/portrait/small/avatar-s-6.jpg')
        },
        {
          name: 'Elicia Rieske',
          img: require('@/assets/images/portrait/small/avatar-s-7.jpg')
        },
        {
          name: 'Darcey Nooner',
          img: require('@/assets/images/portrait/small/avatar-s-8.jpg')
        },
        {
          name: 'Julee Rossignol',
          img: require('@/assets/images/portrait/small/avatar-s-10.jpg')
        },
        {
          name: 'Jeffrey Gerondale',
          img: require('@/assets/images/portrait/small/avatar-s-9.jpg')
        }
      ],
      location: 'Florence, Alabama',
      distance: '168 km',
      distPercent: 70,
      startDate: '26/07/2018',
      estDelDate: '28/07/2018'
    },
    {
      orderNo: 4,
      status: 'Offline',
      statusColor: 'grey',
      operator: 'Anabella Elliott',
      email: 'anabellaelliott.com',
      operatorImg: require('@/assets/images/portrait/small/avatar-s-6.jpg'),
      usersLiked: [
        {
          name: 'Vennie Mostowy',
          img: require('@/assets/images/portrait/small/avatar-s-5.jpg')
        },
        {
          name: 'Elicia Rieske',
          img: require('@/assets/images/portrait/small/avatar-s-7.jpg')
        }
      ],
      location: 'Clifton, Arizona',
      distance: '125 km',
      distPercent: 95,
      startDate: '26/07/2018',
      estDelDate: '28/07/2018'
    }
  ]
}


mock.onGet('/api/table/dispatched-orders').reply(() => {
  return [200, data.dispatchedOrders]
})
