export class MenuMock {
  static root = [
    {
      name: 'dashboard',
      title: 'Dashboard',
      faIcon: 'fa-tachometer',
      link: '/admin-dashboard'
    },
    {
      name: 'teams',
      title: 'Teams',
      faIcon: 'fa-users',
      link: '/view-contestants',
    },
    {
      name: 'matches',
      title: 'Matches',
      faIcon: 'fa-calendar',
      link: '/view-matches',
    }
  ];
}
