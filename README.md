
# ChatApp Api

.Net Core 7.0 ile yapmış olduğum mesajlaşma uygulaması

Projede kullandığım teknolojiler ve kütüphaneler:
- Çok katmanlı mimari(Projeyi parçalara ayırarak daha temiz ve kolay yönetmek için)
- Automapper(Client tarafına nesneleri map'layip gereksiz ve gösterilmemesi gereken objeleri filtrelemek için)
- Entity Framework
- MSSQL(Code first)
- Generic Repository(Veritabanı sorgularını generic bir biçimde tek bir sınıfta oluşturup kod tekrarını azaltmak ve yönetilebilirliği sağlamak için )
- Unit Of Work(Veri tabanı kaydetme işlemini daha yönetilebilir kılmak için)
- Frontend tarafında ise .Net Core 7.0 Blazor wasm
- SignalR:Anlık değişikliği client'a aktarabilmek için.

SOLID prensiplerine olabildiğince uymaya çalıştım.

Frontend: https://github.com/BeginnerDevel0per/ChatApp.UI

## Swagger

![Ekran görüntüsü 2024-05-25 162653](https://github.com/BeginnerDevel0per/ChatApp.Api/assets/106609327/9264503d-f3bd-4846-aa60-8ed01019335b)


  
## API Kullanımı

#### Jwt Token için

```http
  GET api/Login
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `UserName`      | `string` | **Gerekli**.|
| `Password`      | `string` | **Gerekli**.|

- Örnek Kullanım: api/Login?UserName=deneme&Password=1234
- Response: JWT Token.
- kullanıcı girişi gerektiren isteklerde istek başlığına(headers) token eklenmelidir.  


#### Bir Kullanıcıya Mesaj göndermek için
```http
  GET /Message/{SentUserId}/{Message}
```

#### Son Mesajlar


```http
  GET /LastMessages
```
- İstek başlığına(headers) token eklenmelidir.
- Giriş yapan kullanıcının son mesajları döner.  


#### İki kullanıcı arasındaki mesajlar
```http
  GET /GetMessagesBetweenToUsers/{OtherUserId}
```
- İstek başlığına(headers) token eklenmelidir.
- ID'si gönderilen kullanıcıyla giriş yapan kullanıcının tüm mesajlarını döner. 


#### İki kullanıcı arasındaki mesajlar
```http
  GET /SearchUser/{UserName}
```
- İstek başlığına(headers) token eklenmelidir.



#### İki kullanıcı arasındaki mesajlar
```http
  GET /GetProfile
```
- İstek başlığına(headers) token eklenmelidir.
- Giriş yapan kullanıcının profil bilgilerini döner.


#### Profil bilgilerini güncelleme
```http 
  PUT /UpdateProfile
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `email`      | `string` | **Gerekli**.  |
| `userName`      | `string` | **Gerekli**. |
- Parametreler isteğin body kısmına application/json olarak eklenmelidir.
- İstek başlığına(headers) token eklenmelidir.
- Giriş yapan kullanıcı profil bilgilerini güncellemek için bu urlyi kullanmalı ve güncellenmesini istemediği değer varsa eski değeri göndermeli.

#### Şifre değişikliği
```http 
  PUT /UpdatePassword
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `currentPassword`      | `string` | **Gerekli**.  |
| `password`      | `string` | **Gerekli**. |
| `passwordAgain`      | `string` | **Gerekli**. |
- Parametreler isteğin body kısmına application/json olarak eklenmelidir.
- İstek başlığına(headers) token eklenmelidir.
- Giriş yapan kullanıcı Şifre değişikliği için bu urleyi kullanmalı.

#### Arkadaşlık istekleri
```http 
  GET /GetFriendRequests
```
- İstek başlığına(headers) token eklenmelidir.
- Giriş yapan kullanıcıya gelen arkadaşlık isteklerini döner.

#### Profil Görüntüleme
```http 
  GET /GetUserProfile/{GetUserId}
```
- İstek başlığına(headers) token eklenmelidir.
- herhangi kullanıcının profil bilgilerini döner.

#### Arkadaşlık İsteğini Kabul Etme
```http 
  GET /AcceptFriendRequest/{AcceptId}
```
- İstek başlığına(headers) token eklenmelidir.
- Gelen kullanıcı isteğini kabul etmek için bu url kullanılmalıdır.

#### Arkadaşlık İsteği Gönderme
```http 
  GET /SendFriendRequest/{SendId}
```
- İstek başlığına(headers) token eklenmelidir.
- Arkadaşlık isteği göndermek için bu url kullanılmalıdır.


#### Arkadaşlık İsteği veya Ekli Olan Arkadaşı Çıkarma
```http 
  GET /RemoveFriendRequestOrFriend/{RemoveId}
```
- İstek başlığına(headers) token eklenmelidir.

#### Profil Fotoğrafı Yüklemek için
```http 
  Post /UploadProfileImage
```
- Resim dosyası isteğin body  kısmına  multipart/form-data şeklinde eklenmelidir.

- İstek başlığına(headers) token eklenmelidir.

#### Profil Fotoğrafını Kaldırmak için
```http 
  DELETE /RemoveProfileImage
```

- İstek başlığına(headers) token eklenmelidir.


#### Kullanıcıların Profil Fotoğrafını getirmek için
```http 
  GET /UserProfileImage/{ImagePathName}
```

- İstek başlığına(headers) token eklenmelidir.


  