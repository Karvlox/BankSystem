function ViewModel() {
    var self = this;
    self.email = ko.observable('');
    self.password = ko.observable('');
    self.userEmail = ko.observable(localStorage.getItem('userEmail') || '');
    self.userToken = ko.observable(localStorage.getItem('userToken') || '');
    self.userCi = ko.observable(localStorage.getItem('userCi') || '');
    self.isAuthenticated = ko.observable(!!self.userToken());

    self.transactions = ko.observableArray([ 
        {
            receiverId: 12345678,
            transactionTypeId: 'Deuda',
            description: 'Deuda por servicios de internet.',
            amount: 150.75,
            createdAt: '2025-02-10T14:30:57.323Z'
        },
        {
            receiverId: 87654321,
            transactionTypeId: 'Pago',
            description: 'Pago por servicios de electricidad.',
            amount: 75.50,
            createdAt: '2025-02-11T09:15:27.423Z'
        }
    ]);

    function decodeJwt(token) {
        try {
            let base64Url = token.split('.')[1];
            let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            let jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            return JSON.parse(jsonPayload);
        } catch (error) {
            console.error('Error al decodificar el token:', error);
            return null;
        }
    }

    self.login = function() {
        fetch('http://172.20.41.10:5149/autentication/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                email: self.email(),
                password: self.password()
            })
        })
        .then(response => response.json())
        .then(data => {
            if (data.token) {
                localStorage.setItem('userEmail', self.email());
                localStorage.setItem('userToken', data.token);
                self.userEmail(self.email());
                self.userToken(data.token);
                self.isAuthenticated(true);

                let decodedToken = decodeJwt(data.token);
                if (decodedToken && decodedToken.Ci) {
                    localStorage.setItem('userCi', decodedToken.Ci);
                    self.userCi(decodedToken.Ci);
                }
            } else {
                alert('Error en el inicio de sesión');
            }
        })
        .catch(error => alert('Error: ' + error));
    };

    self.logout = function() {
        localStorage.removeItem('userEmail');
        localStorage.removeItem('userToken');
        localStorage.removeItem('userCi');
        self.userEmail('');
        self.userToken('');
        self.userCi('');
        self.isAuthenticated(false);
    };
}

ko.applyBindings(new ViewModel());